using UnityEngine;
using Unity.Jobs;
using Unity.Collections;

public class Example : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DoExample();
    }

    private void DoExample()
    {
        NativeArray<float> resultArray = new NativeArray<float>(1, Allocator.TempJob);

        // Instatiate and Initialize
        SimpleJob myJob = new SimpleJob()
        {
            a = 5f,
            result = resultArray
        };

        DifferentJob myJob2 = new DifferentJob()
        {
            result = resultArray
        };

        // Schedule
        JobHandle myHandle = myJob.Schedule();
        JobHandle myHandle2 = myJob2.Schedule(myHandle); //myHandle is automatically completed within schedule

        // Other task to run in the Main thread

        //myHandle.Complete(); 
        myHandle2.Complete();

        float resultValue = resultArray[0];
        Debug.Log(resultValue);

        resultArray.Dispose();


    }

    private struct SimpleJob : IJob
    {
        public float a;
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = a;
        }
    }

    private struct DifferentJob : IJob
    {
        public NativeArray<float> result;

        public void Execute()
        {
            result[0] = result[0] + 1;
        }
    }
}
