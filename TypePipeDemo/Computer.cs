using System;
using System.Net;

namespace TypePipeDemo
{
  public class Computer
  {
    public virtual void SurfTheWeb (string url)
    {
      var webClient = new WebClient();
      var content = webClient.DownloadString (url);

      if (content.Contains ("shit") || content.Contains ("fuck"))
        Console.WriteLine ("I know what is good for you! Redirect to: www.myLittlePony.com");
      else
        Console.WriteLine ("Go on ... but be carefull, big brother is watching.");
    }

    [Cached]
    public virtual string ComputeMeaningOfLife ()
    {
      Console.WriteLine ("Computing ...");
      return "Meaning of life: 42";
    }

    [Cached]
    public virtual object AnotherOperation ()
    {
      return new object();
    }
  }
}