import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Routes, Route, Link } from 'react-router-dom';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'datatables.net';

import InputMask from 'react-input-mask';
//i have added the API end point here
const API_BASE_URL = 'http://localhost:5074/api';


const Home = () => {
  return <h1 className='d-flex justify-content-center align-items-center'>Advisor Portal.</h1>;
};

const App = () => {
  const [advisors, setAdvisors] = useState([]);
  const [formData, setFormData] = useState({
    name: '',
    sin: '',
    address: '',
    phone: '',
    healthStatus:''
  });
  const [isSinValid, setIsSinValid] = useState(true);

  const [editAdvisorId, setEditAdvisorId] = useState(null);
//will call on every change
  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData({ ...formData, [name]: value });
    if (name === 'sin') {
      checkDuplicateSin(value);
    }
  };
//this function will check in memory any repeated sin is available
  const checkDuplicateSin = (sin) => {
    const existingAdvisor = advisors.find(advisor => advisor.sin === sin);
    setIsSinValid(!existingAdvisor);
  };
//this will update
  const handleEdit = (advisorId) => {
    const confirmEdit = window.confirm('Are you sure you want to edit this advisor?');
    if (confirmEdit) {
      setEditAdvisorId(advisorId);
      const selectedAdvisor = advisors.find(advisor => advisor.id === advisorId);
      if (selectedAdvisor) {
        setFormData({
          name: selectedAdvisor.name,
          sin: selectedAdvisor.sin,
          address: selectedAdvisor.address,
          phone: selectedAdvisor.phone,
          healthStatus: selectedAdvisor.healthStatus,

        });
      }
    }
  };

  const handleSaveChanges = async () => {
    try {
      await axios.put(`${API_BASE_URL}/advisors/${editAdvisorId}`, formData);
      setEditAdvisorId(null);
      setFormData({
        name: '',
        sin: '',
        address: '',
        phone: '',
        healthStatus:''
      });
      fetchAdvisors();
    } catch (error) {
      console.error('Error updating advisor:', error);
    }
  };
//this will call post method
  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await axios.post(`${API_BASE_URL}/advisors`, formData);
      setFormData({
        name: '',
        sin: '',
        address: '',
        phone: '',
        healthStatus:''
      });
      fetchAdvisors();
    } catch (error) {
      console.error('Error adding advisor:', error);
    }
  };
// this is implemented to call delete API
  const handleDelete = async (id) => {
    const confirmDelete = window.confirm('Are you sure you want to delete this advisor?');
    if (confirmDelete) {
      try {
        await axios.delete(`${API_BASE_URL}/advisors/${id}`);
        fetchAdvisors();
      } catch (error) {
        console.error('Error deleting advisor:', error);
      }
    }
  };

  const fetchAdvisors = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/advisors`);
      setAdvisors(response.data);
    } catch (error) {
      console.error('Error fetching advisors:', error);
    }
  };

  useEffect(() => {
    fetchAdvisors();
  }, []);

  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
      </Routes>
     
      <script src="https://cdn.datatables.net/1.11.6/js/jquery.dataTables.js"></script>
      <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

      <div className="container mt-4" style={{ height: '100vh !important' }}>
        <h2>{editAdvisorId ? 'Edit Advisor' : 'Add New Advisor'}</h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-2" >
            <label htmlFor="name" className="form-label">
              Name
            </label>
            <input
              type="text"
              className="form-control"
              id="name"
              name="name"
              value={formData.name}
              onChange={handleChange}
              required
              maxLength="255"
            />
          </div>
          <div className="mb-3">
            <label htmlFor="sin" className="form-label">
              SIN
              <span className="position-absolute top-0 start-100 translate-middle p-2">
      <span className="visually-hidden">Format: 123-456-789</span>
      <i className="bi bi-info-circle"></i>
    </span>
            </label>
            <input
              type="text"
              className="form-control"
              id="sin"
              mask="999-999-999"
              name="sin"
              value={formData.sin}
              onChange={handleChange}
              required
              pattern="\d{9}"
              minLength="9"
              maxLength="9"
              placeholder=" 123456789"
            />
            {!isSinValid && <p className="text-danger">An advisor with the same SIN already exists.</p>}
          </div>
          <div className="mb-3">
            <label htmlFor="address" className="form-label">
              Address
            </label>
            <input
              type="text"
              className="form-control"
              id="address"
              name="address"
              value={formData.address}
              onChange={handleChange}
              maxLength="255"
            />
          </div>
          <div className="mb-3">
            <label htmlFor="phone" className="form-label">
              Phone
            </label>
            <input
              type="text"
              className="form-control"
              id="phone"
              name="phone"
              value={formData.phone}
              onChange={handleChange}
              minLength="8"
              maxLength="8"
              pattern="\d{8}"
              placeholder="Only numbers ex:(12345678)"
            />
              <input
              type="hidden"
              className="form-control"
              id="healthStatus"
              name="healthStatus"
              value={formData.healthStatus}
              onChange={handleChange}
              maxLength="255"
            />
          </div>
          {editAdvisorId ? (
            <button type="button" className="btn btn-primary" onClick={handleSaveChanges} disabled={!isSinValid}  >
              Save Changes
            </button>
          ) : (
            <button type="submit" className="btn btn-primary" disabled={!isSinValid}>
              Add Advisor
            </button>
          )}
        </form>

        {advisors.length > 0 && (
          <div className="mt-4">
            <h2>Advisors List</h2>
            <div className="table-responsive">
              <table className="table table-striped" id="AdvisorsList">
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>SIN</th>
                    <th>Address</th>
                    <th>Phone</th>
                    <th>Health Status</th>
                    <th>Edit</th>
                    <th>Delete</th>
                  </tr>
                </thead>
                <tbody>
                  {advisors.map((advisor) => (
                    <tr key={advisor.id}>
                      <td>{advisor.id}</td>
                      <td>{advisor.name}</td>
                      <td>{advisor.sin.replace(/\d(?=\d{3})/g, '*')}</td>
                      <td>{advisor.address}</td>
                      <td>{advisor.phone}</td>
                      <td style={{ backgroundColor: advisor.healthStatus === 'Red' ? 'red' : advisor.healthStatus === 'Green' ? 'green': advisor.healthStatus === 'Yellow' ? 'Yellow  ' : 'inherit' }}>
                      
                      </td>
                      <td>
                        <button
                          onClick={() => handleEdit(advisor.id)}
                          className="btn btn-primary me-2"
                          >
                          Edit
                        </button>
                          </td>
                        <td>

                        <button
                          onClick={() => handleDelete(advisor.id)}
                          className="btn btn-danger"
                          >
                          Delete
                        </button>
                            </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </div>
        )}
      </div>
    </Router>
  );
};

export default App;
