import { useState } from "react";
import LoginModal from "../Components/LoginModal";

import Section from '../Components/Section';

const sampleItems = [
  { imageSrc: '/assets/test1.jpg', text: 'Text .....' },
  { imageSrc: '/assets/test2.jpg', text: 'More text .....' },
  { imageSrc: '/assets/test3.jpg', text: 'More text againasdasdsa.....' }
];

function AdminHome() {
    const [loggedIn, setLoggedIn] = useState(false);
    const [password, setPassword] = useState("");
    
    return(
        <div>
        <Section title="Activities" items={sampleItems} isAdmin />
             {!loggedIn && <LoginModal setLoggedIn={setLoggedIn} setPassword={setPassword} />}
        <Section title="History" items={sampleItems} isAdmin />
      </div>
    )
}

export default AdminHome;