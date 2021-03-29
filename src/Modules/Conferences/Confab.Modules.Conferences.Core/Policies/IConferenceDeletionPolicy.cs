using System.Threading.Tasks;
using Confab.Modules.Conferences.Core.Entities;

namespace Confab.Modules.Conferences.Core.Policies
{
    internal interface IConferenceDeletionPolicy
    {
        Task<bool> CanDeleteAsync(Conference conference);
    }
}