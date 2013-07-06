using System;

namespace TypePipeDemo
{
  class Program
  {
    static void Main ()
    {
      var computer = new Computer();

      computer.SurfTheWeb ("http://www.commitLogsFromLastNight.com");
      Console.WriteLine();

      var answer = computer.ComputeMeaningOfLife();
      Console.WriteLine (answer);
      Console.WriteLine();
    }
  }
}
