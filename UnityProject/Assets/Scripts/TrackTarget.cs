/*
	Copyright (C) 2020 Anarres

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/> 
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackTarget : MonoBehaviour
{

    public Transform target;
    public bool trackX, trackY, trackZ = true;
    public bool trackRotation = false;
    private Vector3 offset;
    private bool fetched = false;

    // Start is called before the first frame update
    void Start()
    {
        fetchPosition();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(
            trackX ? target.position.x + offset.x : transform.position.x,
            trackY ? target.position.y + offset.y : transform.position.y,
            trackZ ? target.position.z + offset.z : transform.position.z
        );
        if (trackRotation)
        {
            transform.rotation = target.rotation;
        }
    }

    public void fetchPosition()
    {
        Debug.Log(transform.gameObject.name + ": Fetch position!");
        if (fetched) return;
        offset = transform.position - target.position;
        fetched = true;
    }
}
