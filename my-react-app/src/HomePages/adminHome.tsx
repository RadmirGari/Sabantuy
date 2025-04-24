import { useState } from "react";
import LoginModal from "../Components/LoginModal";

function AdminHome() {
    const [loggedIn, setLoggedIn] = useState(false);
    
    return(
        <>
             {!loggedIn && <LoginModal setLoggedIn={setLoggedIn} />}
        </>
    )
}

export default AdminHome;