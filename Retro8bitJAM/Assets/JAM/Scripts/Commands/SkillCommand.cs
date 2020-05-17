using JAM.Scripts.Player;

namespace JAM.Scripts.Commands
{
    public class SkillCommand : Command
    {
        private Minion _minion;

        private void Awake()
        {
            _minion = GetComponent<Minion>();
        }
        public override void Execute()
        {
            base.Execute();
            _minion.CurrentWeapon.Skill();
        }
    }
}