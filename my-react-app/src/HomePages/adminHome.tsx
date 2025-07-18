import { useState } from "react";
import LoginModal from "../Components/LoginModal";

import Section from '../Components/Section';
import Hero from "../Components/Hero";
import Subscribe from "../Components/Subscribe";

const sampleItems = [
  { imageSrc: '/assets/test1.jpg', text: 'Text .....' },
  { imageSrc: '/assets/test2.jpg', text: 'More text .....' },
  { imageSrc: '/assets/test3.jpg', text: 'More text againasdasdsa.....' }
];

function AdminHome() {
    const [loggedIn, setLoggedIn] = useState(false);
    const [password, setPassword] = useState("");
    
    return(
        <>
            {!loggedIn && <LoginModal setLoggedIn={setLoggedIn} setPassword={setPassword} />}
            <div>
                <Hero />
                <div>
                    <Section title="Activities" items={sampleItems} />
                    <Section title="History" items={sampleItems} />
                </div>
                <Subscribe />
            </div>
        </>
    )
}

export default AdminHome;