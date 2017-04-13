using Lx.Utilities.Contract.Infrastructure.DTOs;
using Lx.Utilities.Contract.Infrastructure.Enumerations;
using Lx.Utilities.Contract.Infrastructure.Extensions;
using Lx.Utilities.Modelling.DTOs;

namespace Lx.Utilities.Modelling.Processes {
    public class TrialService : ITrialService {
        public TrialResponse Process(TrialRequest request) {
            return new TrialResponse().LinkTo(request).WithProcessResult(ProcessResultType.Ok);
        }
    }
}