using System.ComponentModel.DataAnnotations.Schema;

namespace Muhasabaa.Domain.Entities.Helpers;

[NotMapped]
public record DailyScore(int earned, int maximum, int percentage);
