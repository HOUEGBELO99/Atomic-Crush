using System;

public class Class1
{
	public Class1()
	{
        IEnumerator SpawnMols()
        {
            yield return new WaitForSeconds(1);
            while (true)
            {
                for (int i = 0; i < moleculesEachWave; i++)
                {
                    Vector3 spawnPosition = new Vector3(Random.Range(spawnValues.x, spawnValues.x + 10), Random.Range(-spawnValues.y, spawnValues.y), Random.Range(spawnValues.z, spawnValues.z + 10));
                    Quaternion spawnRotation = Quaternion.identity;
                    Instantiate(molecule, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(0.1f);
                }
                yield return new WaitForSeconds(3);
            }
        }
    }
}
