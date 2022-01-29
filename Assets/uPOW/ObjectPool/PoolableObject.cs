using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    protected ObjectPool pool;

    public void setPool(ObjectPool pool) {
        this.pool = pool;
    }

    virtual protected void OnDisable() {
        if (pool != null) {
            pool.addToPool(this);
        }
    }
}