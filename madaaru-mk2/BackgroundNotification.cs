using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public class BackgroundNotification {
        public static void Main() {
            DependencyService.Get<INotificationService>().On("タイトル", "SubTitleです", "本文");
        }
    }
}
