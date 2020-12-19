import { useState } from "react";
import { getLoan } from "../util/api";

function SearchLoan() {
  const [loanId, setLoanId] = useState(1);
  const [loan, setLoan] = useState({});

  const searchLoan = (e) => {
    e.preventDefault();

    getLoan(loanId).then((res) => {
      setLoan(res.data);
    });
  };

  return (
    <div className="d.flex flex-row">
      <form style={{ width: "100%" }} onSubmit={searchLoan}>
        <div className="form-group row">
          <div className="col-sm-10">
            <input
              type="number"
              className="form-control"
              placeholder="Loan Id"
              value={loanId}
              onChange={(e) => {
                setLoanId(e.target.value);
              }}
            />
          </div>
          <div className="col-sm-2">
            <button type="submit" className="btn btn-info">
              Search
            </button>
          </div>
        </div>
      </form>
      {Object.keys(loan).length !== 0 && (
        <div className="card mx-1 my-2" style={{ width: "300px" }} key={loan.id}>
          <div className="card-header bg-info text-white text-center">{loan.borrowerName}</div>
          <div className="card-body bg-light">
            <div className="row">
              <div className="col-7">
                <h5 className="card-title">Finance</h5>
                <h5 className="card-title">Repayment</h5>
              </div>
              <div className="col-5 text-right">
                <p className="card-title text-info">${loan.fundingAmount}</p>
                <p className="card-title text-info">${loan.repaymentAmount}</p>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}

export default SearchLoan;
