using System;
using System.Text;
using Subby.Core.Model;

namespace Subby.Core.Service
{
    public interface ISubstitutionService
    {
        void Process(SubstitutionContext context);

        void Process(SubstitutionContext context, Action<SubstitutionContext, StringBuilder> preAction, Action<SubstitutionContext, StringBuilder> postAction);
    }
}
