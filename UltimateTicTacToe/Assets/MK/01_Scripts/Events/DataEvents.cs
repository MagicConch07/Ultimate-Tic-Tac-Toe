public class DataEvent
{

}
public class DataEvents
{
    public static DataEvent VoidDataEvent = new DataEvent();
    public static StringDataEvent StringDataEvent = new StringDataEvent();
    public static FloatDataEvent FloatDataEvent = new FloatDataEvent();
}

public class StringDataEvent : DataEvent
{
    public string data;
}

public class FloatDataEvent : DataEvent
{
    public float data;
}