using System;
using Castle.DynamicProxy;
using Remotion.TypePipe;

namespace TypePipeDemo
{
  class Program
  {
    static void Main ()
    {
      CreateDynamicProxy();

      var pipeline = PipelineFactory.Create (".net open space demo", new CacheParticipant(), new NsaParticipant()); // Put NsaParticipant first!

      var computer = pipeline.Create<Computer>();
      //var computer = new Computer();

      computer.SurfTheWeb ("http://www.commitLogsFromLastNight.com");
      Console.WriteLine();

      Console.WriteLine (computer.ComputeMeaningOfLife());
      Console.WriteLine (computer.ComputeMeaningOfLife());
      Console.WriteLine();

      Console.WriteLine ("Cached: {0}", computer.AnotherOperation() == computer.AnotherOperation());
      Console.WriteLine();

      var assemblyPath = pipeline.CodeManager.FlushCodeToDisk();
      Console.WriteLine (assemblyPath ?? "no assembly generated");
    }

    private static void CreateDynamicProxy ()
    {
      var moduleScope = new ModuleScope (savePhysicalAssembly: true, disableSignedModule: true);
      var proxyBuilder = new DefaultProxyBuilder(moduleScope);
      var proxyGenerator = new ProxyGenerator(proxyBuilder);
      var computer = proxyGenerator.CreateClassProxy<Computer> (new FbiInterceptor());

      computer.SurfTheWeb ("http://www.commitLogsFromLastNight.com");
      Console.WriteLine();

      Console.WriteLine (computer.ComputeMeaningOfLife());
      Console.WriteLine();

      var assemblyPath = proxyGenerator.ProxyBuilder.ModuleScope.SaveAssembly();
      Console.WriteLine (assemblyPath ?? "no assembly generated");

      Console.WriteLine("--------------------------");
      Console.WriteLine();
    }
  }
}
