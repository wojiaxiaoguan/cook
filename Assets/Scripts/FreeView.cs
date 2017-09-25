using UnityEngine;
using System.Collections;

public class FreeView :MonoBehaviour{
    // 观察目标

    public static Transform Target;

    // 观察距离
    public float Distance = 10;

    // 旋转速度
    private float SpeedX = 240;
    private float SpeedY = 200;

    // 角度限制
    private float MinLimitY = -30;
    private float MaxLimitY = 60;

    // 旋转角度
    private float mX = 0.0F;
    private float mY = 0.0F;

    // 鼠标缩放距离最值
    private float MaxDistance = 50;
    private float MinDistance = 10;

    // 鼠标缩放速率
    private float ZoomSpeed = 5F;

    // 是否启用差值
    public bool isNeedDamping = true;

    // 速度
    public float Damping = 10F;

    // 存储角度的四元数
    private Quaternion mRotation;

    // 定义鼠标按键枚举
    private enum MouseButton{
        MouseButton_Left = 0,
        MouseButton_Right = 1,
        MouseButton_Middle = 2
    }

    // 相机移动速度
    private float MoveSpeed = 5.0F;

    // 屏幕坐标
    private Vector3 mScreenPoint;
    
    // 坐标偏移
    private Vector3 mOffset;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        // Gamera.main.fieldOfview = Gamera.main.fieldOfview;

        // 初始化旋转角度
        mX = transform.eulerAngles.x;
        mY = transform.eulerAngles.y;
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        // 鼠标右键旋转
        if (Target != null && Input.GetMouseButton((int)MouseButton.MouseButton_Left))
        {
            // 获取鼠标输入
            mX += Input.GetAxis("Mouse X") * SpeedX * 0.02F;
            mY -= Input.GetAxis("Mouse Y") * SpeedY * 0.02F;

            // 范围限制

            mY = ClampAngle(mY,MinLimitY,MaxLimitY);

            // 计算旋转
            mRotation = Quaternion.Euler(mY,mX,0);

            // 依据是否差值采取不同的角度计算方式
            if (isNeedDamping){
                transform.rotation = Quaternion.Lerp(transform.rotation,mRotation,Time.deltaTime * Damping);
            }else{
                transform.rotation = mRotation;
            }
        }

        //鼠标中键平移  
        //鼠标滚轮缩放
        Distance -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance,MinDistance,MaxDistance);

        //注：Target.position为监视目标的坐标，所以调整监视目标的position就可以调整刚开始时摄像机的位置  
        Vector3 mPosition = mRotation * new Vector3(0.0F, 0.0F, -Distance) + Target.position;  

        if (isNeedDamping){
            transform.position = Vector3.Lerp(transform.position,mPosition,Time.deltaTime * Damping);
        }else{
            transform.position = mPosition;
        }


    }

    private float ClampAngle (float angle ,float min,float max)
    {
        if (angle < -360) angle+=360;
        if (angle > 360) angle -=360;
        return Mathf.Clamp(angle,min,max);
    }
}