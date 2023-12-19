public class SendSignalStation : ControlStation
{
    internal override void CompleteTask()
    {
        print("Signal sent");

        base.CompleteTask();
    }
}