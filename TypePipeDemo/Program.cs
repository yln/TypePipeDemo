using System;
using Remotion.TypePipe;

namespace TypePipeDemo
{
  class Program
  {
    static void Main ()
    {
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
  }
}
