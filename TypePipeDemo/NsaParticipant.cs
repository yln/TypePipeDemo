using System;
using System.Linq;
using System.Reflection;
using Remotion.TypePipe;
using Remotion.TypePipe.Dlr.Ast;
using Remotion.TypePipe.MutableReflection;
using Remotion.TypePipe.MutableReflection.BodyBuilding;
using Remotion.TypePipe.TypeAssembly;

namespace TypePipeDemo
{
  public class NsaParticipant : SimpleParticipantBase
  {
    public override void Participate (object id, IProxyTypeAssemblyContext proxyTypeAssemblyContext)
    {
      Type requestedType = proxyTypeAssemblyContext.RequestedType;
      MutableType proxyType = proxyTypeAssemblyContext.ProxyType;

      var bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
      var auditedMethods = requestedType.GetMethods (bindingFlags);

      foreach (var method in auditedMethods)
        proxyType.GetOrAddOverride (method).SetBody (CreateAuditingBody);
    }

    private Expression CreateAuditingBody (MethodBodyModificationContext ctx)
    {
      // public override void/string MyMethod (int a, sring b) {
      //   NsaParticipant.Audit (methodof(MyMethod), new[] { a, b });
      //   (return) base.MyMethod (a, b);
      // }

      var auditMethod = typeof (NsaParticipant).GetMethod ("Audit");
      return Expression.Block (
          Expression.Call (
              auditMethod,
              Expression.Constant (ctx.BaseMethod),
              Expression.NewArrayInit (typeof (object), ctx.Parameters)),
          ctx.DelegateToBase (ctx.BaseMethod));
    }

    public static void Audit (MethodInfo method, object[] arguments)
    {
      var namedArguments = method.GetParameters().Zip (arguments, (p, a) => p.Name + " = " + a);
      Console.WriteLine ("{0}({1})", method.Name, string.Join (", ", namedArguments));
    }
  }
}