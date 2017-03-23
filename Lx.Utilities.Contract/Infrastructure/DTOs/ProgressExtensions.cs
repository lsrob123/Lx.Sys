namespace Lx.Utilities.Contract.Infrastructure.DTOs {
    public static class ProgressExtensions {
        public static TProgress WithPayload<TProgress>(this TProgress progress, object data)
            where TProgress : Progress {
            progress.Data = data;
            return progress;
        }

        public static TProgress WithUpdate<TProgress>(this TProgress progress, decimal? progressCompleted,
            decimal? progressTotal = null)
            where TProgress : Progress {
            if (progressCompleted.HasValue)
                progress.ProgressCompleted = progressCompleted.Value;
            if (progressTotal.HasValue)
                progress.ProgressTotal = progressTotal.Value;

            return progress;
        }

        public static TProgress WithMessage<TProgress>(this TProgress progress, string message)
            where TProgress : Progress {
            progress.ProgressMessage = message;
            return progress;
        }
    }
}