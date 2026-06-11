using System.Runtime.InteropServices.JavaScript;
using ErrorOr;

namespace Muhasabaa.Domain.Errors;

public class PrayerLogErrors
{
    public static readonly Error InvalidGender  = Error.Validation("PrayerLog.InvalidGender", "only male users can log jamaah prayers");
}