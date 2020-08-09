<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Users_model extends CI_Model 
{
	public function __construct() 
	{
		parent::__construct();

		$this->load->database();
	}

	public function registerUser($user) 
	{
		$data = array(
			'name'   => $user['name'],
			'phone'      => $user['phone'],
			'is_deleted'   => 0,
			'created_at' => date('Y-m-j H:i:s'),
		);
		
		$this->db->select('id');
    	$this->db->from('tbl_users');
    	$this->db->where('phone', $user['phone']);
    	$query = $this->db->get();
		$num = $query->num_rows();
		if ($num > 0) {
			return 0;
		} else {
			$this->db->insert('tbl_users', $data);
			return $this->db->insert_id();
		}
	}

	public function getUserIdFromPhoneNumber($phone) {

		$this->db->select('id');
    	$this->db->from('tbl_users');
    	$this->db->where('phone', $phone);
    	$query = $this->db->get();
		$num = $query->num_rows();
		if ($num > 0) {
			return $query->row()->id;
		} else {
			return 0;
		}
	}
}
