using UnityEngine;
using System.Collections;

public class Helpers
{	
	/* Angle Format: Upper semicircle anticlockwise 0 - 180
	 * 				 Lower semicircle clockwise (-1) - (-179)
	 */
	public static int[] resolveRotation( int angle )		//Returns int[yAxis,zAxis]
	{
		int tempValue = Mathf.Abs( Mathf.RoundToInt( angle ) );
		int yField = ((tempValue / 90) * 180 ) % 360;
		int zField;
		if( tempValue >= 90 )
			zField = ( ( 180 * (int)Mathf.Sign( angle ) ) - angle );
		else
			zField = angle;
		
		return new int[]{ yField, zField };
	}
	
	/* Angle Format: Upper semicircle anticlockwise 0 - 180
	 * 				 Lower semicircle clockwise 359 - 179
	 */
	public static int[] resolveRotation2( int angle )		//Returns int[yAxis,zAxis]
	{
		if( angle > 180 )
		{
			angle -= 360;
		}
		return resolveRotation( angle );
	}
	
	public static int[] getOppositeAngle( int optValueY, int optValueZ, int collOptValueY )
	{
		int zValue, yValue;
		if( ( collOptValueY - optValueY ) < 2 )				//Opposite Direction to instantiate,same y
		{
			//Debug.Log("opposite instantiate");
			yValue = optValueY + 180;
			zValue = 360 - optValueZ;
		}
		else 			//Same direction to instantiate, opposite y
		{
			//Debug.Log("same instantiate");
			yValue = optValueY;
			zValue = optValueZ;
		}
		//Debug.Log("yValue=" + yValue);
		//Debug.Log("zValue=" + zValue);
		return new int[]{ yValue, zValue };
	}
	
	public static float angleCalc(Vector2 a, Vector2 b)		//function to calculate angle
    {
        float diffx = a.x - b.x;
        float diffy = a.y - b.y;
        //Debug.Log(diffx.ToString()+","+diffy.ToString()+"difference="+a.x.ToString()+","+a.y.ToString()+"-"+b.x.ToString()+","+b.y.ToString());
        return ( Mathf.Atan2(diffy, diffx) * Mathf.Rad2Deg );
    }
	
}