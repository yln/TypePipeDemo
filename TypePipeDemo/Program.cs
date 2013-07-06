using System;
using Remotion.TypePipe;

namespace TypePipeDemo
{
  class Program
  {
    static void Main ()
    {
      var pipeline = PipelineFactory.Create (".net open space demo", new NsaParticipant());

      var computer = pipeline.Create<Computer>();
      //var computer = new Computer();

      computer.SurfTheWeb ("http://www.commitLogsFromLastNight.com");
      Console.WriteLine();

      Console.WriteLine ("The meaning of life is {0}.", computer.ComputeMeaningOfLife());
      Console.WriteLine ("The meaning of life is STILL {0}.", computer.ComputeMeaningOfLife());
      Console.WriteLine();

      var assemblyPath = pipeline.CodeManager.FlushCodeToDisk();
      Console.WriteLine (assemblyPath);
    }
  }
}
