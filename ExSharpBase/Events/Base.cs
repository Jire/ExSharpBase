using System.Linq;
using ExSharpBase.Modules;

namespace ExSharpBase.Events
{
    internal class Base
    {
        private Base()
        {
            var ticker = new Timer(0.1f);
            ticker.Elapsed += OnTickElapsed;
            ticker.Start();
            Instance = this;
        }

        private static Base Instance { get; set; } = new Base();

        public static ToggleResult Toggle(TickElapsed value)
        {
            if (OnTick != null)
            {
                if (OnTick.GetInvocationList().Cast<TickElapsed>().Any(s => s == value))
                {
                    OnTick -= value;
                    return ToggleResult.Disabled;
                }
            }

            OnTick += value;
            return ToggleResult.Enabled;
        }

        internal static event TickElapsed OnTick;

        private static void OnTickElapsed(object sender, TimerElapsedEventArgs e)
        {
            OnTick?.Invoke();
        }
    }

    public enum ToggleResult
    {
        Disabled,
        Enabled,
    }

    public delegate void TickElapsed();
}