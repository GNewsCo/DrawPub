using System;
using System.Linq;
using System.Threading.Tasks;
using DrawPub.Core.Log;
using LightInject.Interception;
using ServiceStack.Text;


namespace DrawPub.ImageService.Log
{
    public class LogInterceptor : IInterceptor
    {
        private readonly ILogBase _logger;



        public LogInterceptor(ILogFactory logFactory)
        {
            _logger = logFactory.CreateLog(typeof(LogInterceptor));


        }

        public object Invoke(IInvocationInfo invocationInfo)
        {
            var methodName = invocationInfo.Method.Name;
            var arguments = invocationInfo.Arguments;
            var typeName = invocationInfo.Proxy.Target.ToString();


            try
            {
                if (arguments != null && arguments.Any())
                {
                    _logger.DebugFormat("Begin : Type:{0}, Method:{1}, Args:{2}", typeName, methodName, arguments.Dump());

                }
                else
                {
                    _logger.DebugFormat("Begin : Type:{0}, Method:{1}", typeName, methodName);
                }


                var returnValue = invocationInfo.Proceed();

                var basedType = invocationInfo.Method.ReturnType.BaseType;

                if (basedType == typeof (Task))
                {
                    if (((Task) returnValue).Status == TaskStatus.Faulted)
                    {
                        _logger.ErrorFormat("Failed : Type:{0}, Method:{1}, Exception:{2}", typeName, methodName, ((Task)returnValue).Exception);
                    }
                }

          
                if (returnValue != null && returnValue.GetType().IsPrimitive)
                {
                    _logger.DebugFormat("End : Type:{0}, Method:{1}, Output:{2}", typeName, methodName, returnValue.Dump());
                }
                else
                {
                    _logger.DebugFormat("End : Type:{0}, Method:{1}", typeName, methodName);
                }

                return returnValue;

            }
            catch (Exception ex)
            {
                _logger.ErrorFormat("Failed : Type:{0}, Method:{1}, Exception:{2}", typeName, methodName, ex);
                throw;
            }

        }
    }
}