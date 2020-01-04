using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CubeManager : SingleTon<CubeManager> {

    Cube[,,] cubeArray = new Cube[Global.stageSize.x, Global.stageSize.y, Global.stageSize.z];       // List<List<Cube>> 보다 속도 빠름, 크기 고정이기에 vector 사용 안함

    int[,] heightArray = new int[Global.stageSize.x, Global.stageSize.z];

    List<Cube> disCubeList = new List<Cube>();

    public GameObject particlePrefabs;


    public int GetTargetHeight(Cube cube)
    {
        Int3 pos = new Int3(cube.transform.position);
        int target = heightArray[pos.x, pos.z];
        return target;
    }

    public void Add(Cube cube)
    {
        Int3 pos = new Int3(cube.transform.position);

        //GamOver
        if (pos.y >= Global.stageSize.y)
        {
            GameManager.Instance.GameOver(true);
            return;
        }

        //멈춰있을때만 추가
        if(!cube.IsFall)
        {
            cubeArray[pos.x, pos.y, pos.z] = cube;
            heightArray[pos.x, pos.z]++;
        }
      
    }


    public void CubeFind(Cube cube)
    {
        FindSameCube(cube);

        DisabledCube();
    }


    /// <summary>
    ///     같은 색상의 Cube들 찾기
    /// </summary>
    /// <param name="cube"></param>
    void FindSameCube(Cube cube)
    {
        Int3 index = new Int3(cube.transform.position);
       

        disCubeList.Add(cube);

        for (int i = -1; i <= 1; i= i+2 )
        {
            //배열 인덱스 초과 & Cube 존재 여부 확인
            if (((0 < index.x && i < 0) || (index.x < Global.stageSize.x -1&& i > 0)) && cubeArray[index.x + i, index.y, index.z])
            {
                Cube targetCube = cubeArray[index.x + i, index.y, index.z];
                //같은 색상일 때, 배열에 포함 여부 확인 후 재귀 호출
                if (targetCube.color == cube.color && !disCubeList.Contains(targetCube))
                {
                    FindSameCube(targetCube);
                }
            }
        }

        for (int i = -1; i <= 1; i = i + 2)
        {
            if (((0 < index.y && i < 0) || (index.y < Global.stageSize.y -1&& i > 0)) && cubeArray[index.x, index.y + i, index.z])
            {
                Cube targetCube = cubeArray[index.x, index.y + i, index.z];
                if (targetCube.color == cube.color && !disCubeList.Contains(targetCube))
                {
                    FindSameCube(targetCube);
                }
            }
        }

        for (int i = -1; i <= 1; i = i + 2)
        {
            if (((0 < index.z && i < 0) || (index.z < Global.stageSize.z-1 && i > 0)) && cubeArray[index.x, index.y, index.z + i])
            {
                Cube targetCube = cubeArray[index.x, index.y, index.z + i];
                if (targetCube.color == cube.color && !disCubeList.Contains(targetCube))
                {
                    FindSameCube(targetCube);
                }
            }
        }

    }

    /// <summary>
    ///  Cube return to PoolManager & Score Add
    /// </summary>
    void DisabledCube()
    {

        if (disCubeList.Count > 1)
        {
            for (int i = 0; i < disCubeList.Count; ++i)
            {
                Int3 pos = new Int3(disCubeList[i].transform.position);

                //큐브 반환하고 배열에서 제거
                PoolManager.Instance.PushToPool(disCubeList[i].color, disCubeList[i].gameObject, PoolManager.Instance.parents[disCubeList[i].color]);
                cubeArray[pos.x, pos.y, pos.z] = null;

                ParticleSystem particle = Instantiate(disCubeList[i].particlePrefabs, disCubeList[i].transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
                particle.Play();

                //해당 큐브라인 target 1감소
                heightArray[pos.x, pos.z]--;

                //쌓여있는 큐브들 다시 떨어짐
                UpperCubeFall(disCubeList[i]);
            }
            //점수 추가
            ScoreManager.Instance.Add(disCubeList.Count);

        }
        disCubeList.Clear();
    }



    void UpperCubeFall(Cube cube)
    {
        Int3 pos = new Int3(cube.transform.position);

        for (int upper = pos.y + 1; upper < Global.stageSize.y; ++upper)
        {
            //반환된 큐브 위에 쌓여있는 큐브들이 있고, 반환될 대상이 아니라면 내려오게 설정
            if (cubeArray[pos.x, upper, pos.z] && !disCubeList.Contains(cubeArray[pos.x, upper, pos.z]))
            {
                heightArray[pos.x, pos.z]--;
                cubeArray[pos.x, upper, pos.z].IsFall = true;
                cubeArray[pos.x, upper, pos.z] = null;
            }
        }
      
    }
 
}
