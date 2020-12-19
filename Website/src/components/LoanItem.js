import {deleteLoan, getLoan} from "../util/api";
import {useEffect, useState} from "react";

function LoanItem(props) {
  const [loanItems, setLoanItems] = useState(props.loans);

  useEffect(() => {
    setLoanItems(props.loans);
  }, [props.loans]);
  
  const onDeleteLoan = (id) => {
    deleteLoan(id).then(res => {
      props.deleteLoan(id);
    })
  };
  
  return loanItems
    .filter((loan) => !loan.deleted)
    .map((loan) => {
      return (
        <div className="card mx-1 my-2" style={{ width: "250px" }} key={loan.id}>
          <div className="card-header bg-info text-white text-center">{loan.id} - {loan.borrowerName}</div>
          <div className="card-body bg-light">
            <div className="row">
              <div className="col">
                <div className="d-flex flex-row justify-content-between">
                  <h5 className="card-title">Finance</h5>
                  <p className="card-title text-info">${loan.fundingAmount}</p>
                </div>
              </div>
              <div className="col">
                <div className="d-flex flex-row justify-content-between">
                  <h5 className="card-title">Repayment</h5>
                  <p className="card-title text-info">${loan.repaymentAmount}</p>
                </div>
              </div>
            </div>

            <div className="row justify-content-end px-3">
              <button
                className="btn btn-danger btn-sm"
                type="button"
                onClick={() => {
                  onDeleteLoan(loan.id);
                }}>
                Delete
              </button>
            </div>
          </div>
        </div>
      );
    });
}

export default LoanItem;
