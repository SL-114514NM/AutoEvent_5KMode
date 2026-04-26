using LabApi.Features.Wrappers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoEvent_5KMode.Cores
{
    public class MainContinue
    {
        public static IEnumerable<float> NBContinue()
        {
            yield return Timing.WaitForSeconds(1);
            while (Round.IsRoundStarted)
            {

                yield return Timing.WaitForSeconds(1);
            }
        }
    }
}
