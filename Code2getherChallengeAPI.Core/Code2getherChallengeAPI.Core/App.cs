using Code2getherChallengeAPI.Core.ViewModels;
using MvvmCross.ViewModels;

namespace Code2getherChallengeAPI.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<AppViewModel>();
        }
    }
}