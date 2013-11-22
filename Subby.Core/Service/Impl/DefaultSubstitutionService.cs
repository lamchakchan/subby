using System;
using System.Text;
using Subby.Core.Model;

namespace Subby.Core.Service.Impl
{
    public class DefaultSubstitutionService : ISubstitutionService
    {
        public void Process(SubstitutionContext context)
        {
            Process(context, null, null);
        }

        public void Process(SubstitutionContext context, Action<SubstitutionContext, StringBuilder> preAction, Action<SubstitutionContext, StringBuilder> postAction)
        {
            if (context == null)
            {
                throw new Exception("context is null");
            }

            if (context.Variables == null)
            {
                throw new Exception("Variables dictionary is null");
            }

            if (context.Target == null)
            {
                throw new Exception("Target is null");
            }

            var builder = new StringBuilder(context.Target);

            if (preAction != null)
            {
                preAction(context, builder);
            }

            foreach (var key in context.Variables.Keys)
            {
                builder.Replace(key, context.Variables[key]);
            }

            if (postAction != null)
            {
                postAction(context, builder);
            }

            context.Result = builder.ToString();
        }
    }
}
