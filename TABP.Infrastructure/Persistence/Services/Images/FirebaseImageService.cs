using Firebase.Storage;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Persistence.Services;

namespace TABP.Infrastructure.Persistence.Services.Images;

public class FirebaseImageService : IImageService
{
  private static readonly string[] AllowedImageFormats = [".jpg", ".jpeg", ".png"];
  private readonly FirebaseConfig _firebaseConfig;

  public FirebaseImageService(IOptions<FirebaseConfig> fireBaseConfig)
  {
    _firebaseConfig = fireBaseConfig.Value;
  }

  public async Task<Image> StoreAsync(IFormFile image, CancellationToken cancellationToken = default)
  {
    if (image is null || image.Length <= 0) throw new ArgumentNullException();

    var imageFormat = image.ContentType.Split('/')[1];

    if (!IsAllowedImageFormat(imageFormat)) throw new ArgumentOutOfRangeException();

    var credential = GoogleCredential.FromJson(_firebaseConfig.CredentialsJson);

    var storage = await StorageClient.CreateAsync(credential);

    var imageModel = new Image { Format = imageFormat };

    var destinationObjectName = $"{imageModel.Id}.{imageFormat}";

    await storage.UploadObjectAsync(
      _firebaseConfig.Bucket,
      destinationObjectName,
      image.ContentType,
      image.OpenReadStream(),
      null,
      cancellationToken);

    imageModel.Path = await GetImagePublicUrl(destinationObjectName);

    return imageModel;
  }
  
  private static bool IsAllowedImageFormat(string imageFormat) => AllowedImageFormats.Contains(imageFormat);
  
  private async Task<string> GetImagePublicUrl(string destinationObjectName)
  {
    var storage = new FirebaseStorage(_firebaseConfig.Bucket);

    var starsRef = storage.Child(destinationObjectName);

    return await starsRef.GetDownloadUrlAsync();
  }
  
  public async Task DeleteAsync(Image image, CancellationToken cancellationToken = default)
  {
    var storage = new FirebaseStorage(_firebaseConfig.Bucket);

    await storage.Child($"{image.Id}.{image.Format}").DeleteAsync();
  }
}