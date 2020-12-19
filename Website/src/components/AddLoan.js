import React, { useState } from "react";
import { addLoan } from "../util/api";

const initialState = {
  borrowerName: "",
  repaymentAmount: "0",
  fundingAmount: "0",
};

function AddLoan({updateLoans}) {
  const [newLoan, setNewLoan] = useState(initialState);

  const onChange = (e) => {
    setNewLoan({ ...newLoan, [e.target.name]: e.target.value });
  };

  const createNewLoan = (e) => {
    e.preventDefault();

    newLoan.repaymentAmount = parseFloat(newLoan.repaymentAmount);
    newLoan.fundingAmount = parseFloat(newLoan.fundingAmount);

    addLoan(newLoan)
        .then(res => {
          updateLoans(res.data);
          setNewLoan(initialState);
        })
  };

  const calculateLoan = () => {
    //Calculate repayment amount for UI view
    let repaymentAmount = (parseFloat(newLoan.fundingAmount) * 1.2).toFixed(2);
    setNewLoan({ ...newLoan, repaymentAmount });
  };

  return (
    <form onSubmit={createNewLoan}>
      <div className="form-group row">
        <label className="col-sm-4 col-form-label">Name</label>
        <div className="col-sm-8">
          <input
            type="text"
            name="borrowerName"
            value={newLoan.borrowerName}
            {...{ onChange }}
            className="form-control"
          />
        </div>
      </div>

      <div className="form-group row">
        <label className="col-sm-4 col-form-label">Funding Amount</label>
        <div className="col-sm-8">
          <input
            type="text"
            name="fundingAmount"
            value={newLoan.fundingAmount}
            {...{ onChange }}
            className="form-control"
          />
        </div>
      </div>

      <fieldset disabled>
        <div className="form-group row">
          <label className="col-sm-4 col-form-label">Repayment Amount Calculated</label>
          <div className="col-sm-8">
            <input type="text" className="form-control" value={newLoan.repaymentAmount} readOnly />
          </div>
        </div>
      </fieldset>

      <div className="form-group row mr-0 justify-content-end">
        <button type="button" className="btn btn-info mr-3" onClick={calculateLoan}>
          Calculate
        </button>
        <button type="submit" className="btn btn-primary">
          Create Loan
        </button>
      </div>
    </form>
  );
}

export default AddLoan;
