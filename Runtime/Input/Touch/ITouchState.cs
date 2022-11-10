using UnityEngine;

public interface ITouchState
{
    public void Begin(Vector2 input);
    public void Touch(Vector2 input);
    public void End(Vector2 input);
}