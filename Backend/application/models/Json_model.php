<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Json_model extends CI_Model 
{
	public function __construct() 
	{
		parent::__construct();

		$this->load->database();
	}

	public function insertJson($json) 
	{
		$data = array(
			'housetype' => $json['housetype'], 
            'rooftype' => $json['rooftype'], 
            'bricktype' => $json['bricktype'], 
            'bigdogs' => $json['bigdogs'], 
            'smalldogs' => $json['smalldogs'], 
            'cats' => $json['cats'], 
            'sportballs' => $json['sportballs'], 
            'yardsign' => $json['yardsign'], 
            'panelcount' => $json['panelcount'], 
            'percentsavings' => $json['percentsavings'], 
            'averagebill' => $json['averagebill'], 
            'created_by' => $json['created_by'], 
            'created_at' => date('Y-m-d H:i:s')
		);
		
		$this->db->insert('tbl_json', $data);
		return $this->db->insert_id();
	}
}
