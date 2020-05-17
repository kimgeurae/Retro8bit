using JAM.Scripts.Managers;

namespace JAM.Scripts.Commands
{
    public class SwitchCharacterCommand : Command
    {
        private CharacterManager _characterManager;
        
        public override void Execute()
        {
            base.Execute();
            CharacterManager.SwitchCharacter();
        }
    }
}