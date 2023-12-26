public class SendSignalStation : ControlStation
{
    public override void CompleteTask()
    {
        print("Signal sent");

        base.CompleteTask();
    }
}