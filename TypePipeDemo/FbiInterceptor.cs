using Castle.DynamicProxy;

namespace TypePipeDemo
{
  public class FbiInterceptor : IInterceptor
  {
    public void Intercept (IInvocation invocation)
    {
      NsaParticipant.Audit (invocation.MethodInvocationTarget, invocation.Arguments);
      invocation.Proceed();
    }
  }
}