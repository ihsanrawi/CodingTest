import Axios from "axios";

const BASE_URL = "https://localhost:5001/api";

export const addLoan = async (newLoan) => {
  return await Axios.post(`${BASE_URL}/loan`, newLoan);
};

export const deleteLoan = async (id) => {
  return await Axios.delete(`${BASE_URL}/loan/${id}`);
};

export const getLoan = (id = 0) => {
  // get all loan if id is not specified
  if (id === 0) {
    return Axios.get(`${BASE_URL}/loan`);
  }

  //Get loan by id
  return Axios.get(`${BASE_URL}/loan/${id}`);
};
