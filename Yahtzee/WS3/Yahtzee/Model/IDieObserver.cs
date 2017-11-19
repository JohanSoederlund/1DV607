
namespace Yahtzee.Model
{
    interface IDieObserver
    {
        void DieRolled(int[] dieValues, int[] die);
    }
}
