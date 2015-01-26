
namespace PatientDesktopClient.Readers
{
    public delegate void GestureReadHandler();

    sealed class GestureReader
    {

        public static readonly GestureReader Instance = new GestureReader();
        public event GestureReadHandler GestureRead;

        private GestureReader()
        {

        }

    }
}
