using System.Threading.Tasks;
using Xamarin.Forms;

namespace madaarumk2 {
    public class BackgroundNotification {
        public static void Main(string msg) {
            DependencyService.Get<INotificationService>().On("まだある？", "なくなりそうです！", msg);
        }
    }
}
