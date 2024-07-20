namespace Dialog
{
    public class Dialog : IDialog
    {
        private int _currentDialogIndex;
        private readonly string[] _texts;

        public Dialog(DialogData dialogData)
        {
            _texts = dialogData.dialogText.ToArray();
        }

        
        
        public bool HasNextDialog()
        {
            return _currentDialogIndex < _texts.Length;
        }

        public string GetNextDialog()
        {
            if (!HasNextDialog()) return null;
            var dialog = _texts[_currentDialogIndex];
            _currentDialogIndex++;

            return dialog;
        }
    }
}