using Lx.Utilities.Modelling.DTOs;

namespace Lx.Utilities.Modelling.Processes
{
    public interface ITrialService
    {
        TrialResponse Process(TrialRequest request);
    }
}