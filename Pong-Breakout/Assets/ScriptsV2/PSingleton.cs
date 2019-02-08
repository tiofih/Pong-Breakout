using UnityEngine;

public class PSingleton<T> : MonoBehaviour where T : class
{

    private static T privateInstance;

    public static T Instance => PSingleton<T>.privateInstance;

    private void Awake()
    {
        if (PSingleton<T>.privateInstance == null)
        {
            PSingleton<T>.privateInstance = (T)(object)this;
        }
        else if (PSingleton<T>.privateInstance != (T)(object)this)
        {
            Debug.LogWarning($"Existe mais de uma instancia do script {typeof(T).Name} tentando executar. Apagando{this.gameObject.name}");
            Object.Destroy(this);
        }
    }

    private void OnDestroy()
    {
        this.DestroySingleton();
    }

    private void DestroySingleton()
    {
        PSingleton<T>.privateInstance = null;
    }
}
