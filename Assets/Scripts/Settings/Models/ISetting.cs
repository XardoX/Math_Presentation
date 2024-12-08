
public interface ISetting<T>
{
    public abstract void SetValue(T value);

    public abstract T GetValue();

    protected abstract void OnValueChanged(T oldValue);
}
