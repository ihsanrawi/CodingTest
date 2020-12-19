import { useState, useEffect } from "react";

import LoanItem from "./components/LoanItem";
import AddLoan from "./components/AddLoan";
import SearchLoan from "./components/SearchLoan";
import { getLoan } from "./util/api";

function App() {
  const [loans, setLoans] = useState([]);

  useEffect(() => {
    getLoan().then(({ data }) => {
      setLoans(data);
    });
  }, []);

  function updateLoans(newLoan) {
    setLoans([...loans, {...newLoan}])
  }
  
  function deleteLoan(id) {
    let newLoans = loans.filter(loan => loan.id !== id)
    setLoans(newLoans);
  }

  return (
    <div className="container">
      <div className="row border-bottom border-info ">
        <div className="col-8">
          <div className="d-flex flex-column">
            <div className="my-3">
              <h4>Add New Loan</h4>
            </div>
              <AddLoan {...{updateLoans}}/>
          </div>
        </div>
        <div className="col-4">
          <div className="my-3">
            <h4>Search Loan</h4>
          </div>
          <div className="d-flex">
            <SearchLoan />
          </div>
        </div>
      </div>
      <div className="row py-3 flex-column">
        <h4>Loans</h4>
        <div className="container">
          <div className="d-flex flex-row">
            <LoanItem {...{ loans,deleteLoan }} />
          </div>
        </div>
      </div>
    </div>
  );
}

export default App;
