<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class API extends CI_Controller
{
	public function __construct() 
	{
		parent::__construct();

        $this->load->library(array('session'));
        $this->load->model('users_model');
        $this->load->model('json_model');
        $this->load->model('userjson_model');
	}

    public function registerUser() {
        $name = $this->input->post('name');
        $phone_number = $this->input->post('phone_number');
        
        $data = array(
            'name' => $name, 
            'phone' => $phone_number
        );

        $result = $this->users_model->registerUser($data);
        echo json_encode($result);
    }

    public function shareJson() {
        $from = $this->input->post('from');
        $tophone = $this->input->post('to');
        $json_id = $this->input->post('json');

        $to = $this->users_model->getUserIdFromPhoneNumber($tophone);

        $data = array(
            'user_id' => $to, 
            'json_id' => $json_id, 
            'shared_by' => $from, 
            'shared_at' => date('Y-m-d H:i:s')
        );

        $result = $this->userjson_model->shareJson($data);
        echo json_encode($result);
    }

    public function saveJson() {
        $housetype = $this->input->post('housetype');
        $rooftype = $this->input->post('rooftype');
        $bricktype = $this->input->post('bricktype');
        $bigdogs = $this->input->post('bigdogs');
        $smalldogs = $this->input->post('smalldogs');
        $cats = $this->input->post('cats');
        $sportballs = $this->input->post('sportballs');
        $yardsign = $this->input->post('yardsign');
        $panelcount = $this->input->post('panelcount');
        $percentsavings = $this->input->post('percentsavings');
        $averagebill = $this->input->post('averagebill');
        $created_by = $this->input->post('userid');

        $data = array(
            'housetype' => $housetype, 
            'rooftype' => $rooftype, 
            'bricktype' => $bricktype, 
            'bigdogs' => $bigdogs, 
            'smalldogs' => $smalldogs, 
            'cats' => $cats, 
            'sportballs' => $sportballs, 
            'yardsign' => $yardsign, 
            'panelcount' => $panelcount, 
            'percentsavings' => $percentsavings, 
            'averagebill' => $averagebill, 
            'created_by' => $created_by
        );

        $json_id = $this->json_model->insertJson($data);

        $data = array(
            'user_id' => $created_by, 
            'json_id' => $json_id, 
            'shared_by' => '0'
        );

        $result = $this->userjson_model->shareJson($data);
        if ($result > 0)
            echo json_encode($json_id);
        else
            echo json_encode('');
    }

    public function getJsonByUserId() {
        $userid = $this->input->post('userid');

        $result = $this->userjson_model->getJson($userid);
        echo json_encode($result);
    }
}
