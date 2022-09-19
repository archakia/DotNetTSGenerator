namespace lib.test.a.b.Vehicles.TwoWheeled.MotorCycles
{
    public class Harley<T> : IHog<T>
    {
        public Trim MyTrim { get; set; }
        public T Type { get; set; }
    }

    public enum Trim
    {
        Chrome,
        Matte
    }

    public interface IHog<T>
    {

    }
}