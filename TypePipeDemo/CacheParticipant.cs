using System;
using System.Reflection;
using Remotion.TypePipe;
using Remotion.TypePipe.Dlr.Ast;
using Remotion.TypePipe.MutableReflection;
using Remotion.TypePipe.MutableReflection.BodyBuilding;
using Remotion.TypePipe.TypeAssembly;
using System.Linq;

namespace TypePipeDemo
{
  public class CacheParticipant : SimpleParticipantBase
  {
    public override void Participate (object id, IProxyTypeAssemblyContext proxyTypeAssemblyContext)
    {
      var proxy = proxyTypeAssemblyContext.ProxyType;

      var cachedMethods = proxyTypeAssemblyContext
          .RequestedType.GetMethods()
          .Where (m => m.IsDefined (typeof (Cached), inherit: true));

      foreach (var method in cachedMethods)
      {
        var cacheField = proxy.AddField ("_cache_" + method.Name, FieldAttributes.Private, method.ReturnType);
        proxy.GetOrAddOverride (method).SetBody (ctx => CreateCachingBody (ctx, cacheField));
      }
    }

    private Expression CreateCachingBody (MethodBodyModificationContext ctx, MutableFieldInfo cacheField)
    {
      // if (_cache_field == null) {
      //   _cache_field = <previous body>;
      // }
      // return _cache_field;

      var field = Expression.Field (ctx.This, cacheField);
      return Expression.Block (
          Expression.IfThen (
              Expression.Equal (field, Expression.Constant (null)),
              Expression.Assign (field, ctx.PreviousBody)),
          field);
    }
  }
}