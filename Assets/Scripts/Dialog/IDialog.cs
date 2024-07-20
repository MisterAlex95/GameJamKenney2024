namespace Dialog
{
    public interface IDialog
    {
        string GetNextDialog();
        bool HasNextDialog();
    }
}