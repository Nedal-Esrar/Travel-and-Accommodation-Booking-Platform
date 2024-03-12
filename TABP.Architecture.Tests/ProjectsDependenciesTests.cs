using System.Reflection;
using FluentAssertions;
using NetArchTest.Rules;
using static TABP.Architecture.Tests.Constants;

namespace TABP.Architecture.Tests;

public class ProjectsDependenciesTests
{
  [Fact]
  public void Domain_ShouldNotHaveDependencyOn_AnyLayer()
  {
    var assembly = Assembly.Load(DomainAssembly);

    var otherProjects = new[]{
      ApplicationNamespace,
      InfrastructureNamespace,
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
  
  [Fact]
  public void Application_ShouldNotHaveDependencyOn_ExternalLayers()
  {
    var assembly = Assembly.Load(ApplicationAssembly);

    var otherProjects = new[]{
      InfrastructureNamespace,
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
  
  [Fact]
  public void Infrastructure_ShouldNotHaveDependencyOn_OtherExternalLayers()
  {
    var assembly = Assembly.Load(InfrastructureAssembly);

    var otherProjects = new[]{
      ApiNamespace
    };

    var result = Types.InAssembly(assembly)
      .ShouldNot()
      .HaveDependencyOnAll(otherProjects)
      .GetResult();

    result.IsSuccessful.Should().BeTrue();
  }
}