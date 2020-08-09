<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Userjson_model extends CI_Model 
{
	public function __construct() 
	{
		parent::__construct();

		$this->load->database();
	}

	public function shareJson($json) 
	{
		$data = array(
			'user_id' => $json['user_id'], 
            'json_id' => $json['json_id'], 
            'shared_by' => $json['shared_by'],  
            'shared_at' => date('Y-m-d H:i:s')
		);
		
		$this->db->insert('tbl_userjson', $data);
		return $this->db->insert_id();
	}

	public function getJson($userid) {
		$this->db->select('tbl_json.housetype, tbl_json.rooftype, tbl_json.bricktype, tbl_json.bigdogs, tbl_json.smalldogs, tbl_json.cats, tbl_json.sportballs');
		$this->db->from('tbl_userjson');
		$this->db->join('tbl_json', 'tbl_userjson.json_id = tbl_json.id', 'left');
		$this->db->where('tbl_userjson.user_id', $userid);
		return (object)($this->db->get()->result());
	}
}
