internal class ActionData
{
    private string GestureName { get; set; }
    private int GestureID { get; set; }
    private string GifPath { get; set; }

    public ActionData(string gestureName, int gestureID, string gifPath)
    {
        GestureName = gestureName;
        GestureID = gestureID;
        GifPath = gifPath;
    }

}