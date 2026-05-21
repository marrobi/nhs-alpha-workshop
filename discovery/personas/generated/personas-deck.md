---
marp: true
theme: default
paginate: true
size: 16:9
style: |
  section, section * {
    box-sizing: border-box;
  }

  section {
    display: flex;
    flex-direction: column;
    padding: 0.7rem;
    font-size: 0.8rem;
    height: 100%;
    overflow: hidden;
  }
  
  .persona-header {
    text-align: center;
    margin-bottom: 0.8rem;
    flex-shrink: 0;
  }
  
  .persona-title {
    font-size: 2rem;
    font-weight: bold;
    color: #00838F;
    margin: 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-subtitle {
    font-size: 0.8rem;
    color: #666;
    margin: 0.2rem 0 0 0;
    text-transform: uppercase;
    letter-spacing: 1px;
  }
  
  .persona-content {
    display: flex;
    gap: 1rem;
    flex: 1;
    min-height: 0;
    align-items: stretch;
    height: 100%;
  }

  .profile-column {
    flex: 0 0 260px;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    height: 100%;
    min-height: 0;
    max-height: 100%;
    align-self: stretch;
    overflow: hidden;
  }
  
  .profile-section {
    background: white;
    border: 3px solid #00838F;
    border-radius: 10px;
    padding: 0.6rem 0.75rem;
    text-align: center;
    box-shadow: 0 2px 6px rgba(0,0,0,0.1);
    display: flex;
    flex-direction: column;
    gap: 0.5rem;
    flex: 1 1 0;
    min-height: 0;
    overflow: hidden;
  }
  
  .sample-text {
    font-size: 0.55rem;
    color: #999;
    margin-bottom: 0.4rem;
    background: #f5f5f5;
    padding: 0.1rem 0.3rem;
    border-radius: 3px;
  }
  
  .profile-section img {
    width: 120px;
    height: 120px;
    border-radius: 50%;
    border: 2px solid #00838F;
    margin: 0 auto;
    flex-shrink: 0;
    object-fit: cover;
  }
  
  .persona-name {
    font-size: 1.1rem;
    font-weight: bold;
    color: #00838F;
    margin: 0.2rem 0 0.1rem 0;
    text-transform: uppercase;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .job-title {
    color: #666;
    font-weight: bold;
    margin-bottom: 0.4rem;
    font-size: 0.75rem;
    line-height: 1.0;
    flex-shrink: 0;
  }
  
  .personal-data {
    flex: 1 1 0;
    overflow-y: auto;
    padding-right: 0.2rem;
    display: flex;
    flex-direction: column;
    gap: 0.4rem;
    min-height: 4rem;
  }

  .personal-data > div {
    background: transparent;
    border-radius: 0;
    padding: 0;
    border-left: none;
    box-shadow: none;
  }

  .personal-data h4 {
    color: #00838F;
    font-size: 0.75rem;
    margin: 0;
    text-transform: uppercase;
    text-align: left;
    letter-spacing: 0.5px;
  }

  .persona-facts {
    display: flex;
    flex-direction: column;
    gap: 0.25rem;
  }

  .data-item {
    display: flex;
    align-items: flex-start;
    gap: 0.35rem;
    text-align: left;
    font-size: 0.68rem;
    line-height: 1.2;
    color: #1f1f1f;
  }

  .data-icon {
    font-size: 0.8rem;
    line-height: 1;
    flex-shrink: 0;
    margin-top: 0.1rem;
  }

  .data-item span:last-child {
    flex: 1;
  }

  .personal-data ul {
    text-align: left;
    font-size: 0.7rem;
    line-height: 1.2;
    margin: 0;
    padding-left: 0.9rem;
  }

  .personal-data li {
    margin-bottom: 0.2rem;
    color: #333;
  }
  
  .content-columns {
    display: flex;
    flex: 1;
    gap: 1.2rem;
    min-height: 0;
  }
  
  .content-single-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    gap: 0.6rem;
    min-height: 0;
  }
  
  .content-column {
    flex: 1;
    display: flex;
    flex-direction: column;
    min-height: 0;
  }
  
  .content-box {
    background: rgba(0, 131, 143, 0.05);
    border-radius: 6px;
    padding: 0.7rem;
    margin-bottom: 0.6rem;
    border-left: 3px solid #00838F;
    flex-shrink: 0;
  }
  
  .section-title {
    font-size: 0.9rem;
    font-weight: bold;
    color: #00838F;
    margin: 0 0 0.4rem 0;
    text-transform: uppercase;
    line-height: 1.1;
  }
  
  .content-box ul {
    margin: 0;
    padding-left: 0.8rem;
  }
  
  .content-box li {
    margin-bottom: 0.2rem;
    line-height: 1.1;
    color: #333;
    font-size: 0.75rem;
  }
  
  .skills-box {
    background: rgba(0, 131, 143, 0.05);
    border-radius: 8px;
    padding: 1rem;
    border-left: 3px solid #00838F;
    flex: 1;
    min-height: 0;
  }
  
  .skill-item {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin: 0.4rem 0;
    font-size: 0.8rem;
  }
  
  .stars {
    color: #00838F;
    font-size: 0.9rem;
  }
  
  .systems-grid {
    display: flex;
    justify-content: space-between;
    margin-top: 0.6rem;
    gap: 0.3rem;
  }
  
  .system-badge {
    background: #00838F;
    color: white;
    padding: 0.3rem 0.5rem;
    border-radius: 4px;
    font-weight: bold;
    font-size: 0.7rem;
    text-align: center;
    flex: 1;
  }
  
  .logo-container {
    display: flex;
    flex-direction: column;
    align-items: center;
    gap: 0.4rem;
    flex-shrink: 0;
    height: auto;
    padding: 0.3rem 0;
  }

  .logo-container img {
    max-width: 200px;
    height: auto;
    object-fit: contain;
    flex-shrink: 0;
  }

  .logo-container img:first-child {
    max-height: 28px;
  }

  .logo-container img:last-child {
    max-height: 20px;
  }

---
# NHS TRUST VACCINATION COORDINATOR

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Chioma Adebayo</h2>
    <p class="job-title">COVID-19 Vaccination Coordinator — Band 8a</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Band 5 staff nurse since 2014, vaccination ops since 2021</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>West Midlands</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Large NHS Acute Trust — Vaccination Programme</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Coordinates COVID-19 vaccine across hospital hub and 3 community sites</li>
          <li>Bulk-onboards ~25 staff each autumn (locums, vaccinators, admin)</li>
          <li>Power user — designed own Power BI dashboard for stock vs demand</li>
          <li>Nigerian-British; English first language at work</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Bulk-onboard 25 new joiners in a day, not stagger over five weeks</li>
          <li>No clinic cancelled because a staff member couldn't access ImmForm in time</li>
          <li>Reconcile stock to dose-administered records with audit trail</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>API or bulk CSV import for staff registrations</li>
          <li>Visibility into helpdesk queue depth to decide whether to escalate or wait</li>
          <li>Machine-readable receipts for reconciliation spreadsheets</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current PDF + email + 5-day SLA does not scale for 20–30 onboardings in 2 weeks</li>
          <li>One registration delay can hold up a 1,200-dose clinic</li>
          <li>Approver-signature chase: Chief Pharmacist signing dozens of PDFs in same window</li>
    </ul>
  </div>

</div>

</div>



---

# PRACTICE NURSE

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Daniel Okonkwo</h2>
    <p class="job-title">Practice Nurse (Named RHCP) — Band 5</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>NMC-registered nurse, immunisation specialist</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Suburban Leicester</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Cherrywood Medical Centre — Immunisation Clinic</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Named Registered Healthcare Practitioner for immunisation</li>
          <li>NMC revalidation every three years</li>
          <li>Nigerian heritage; trilingual (English, Igbo, conversational Yoruba)</li>
          <li>Mild dyslexia — relies on plain language and generous spacing</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Administer vaccinations safely with direct ImmForm ordering ability</li>
          <li>Complete registration before first clinical day at a new practice</li>
          <li>Maintain PGD signatory record and annual immunisation training</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Confirmation screens that are easy to re-read (dyslexia-friendly)</li>
          <li>Clear, plain-language guidance throughout the registration journey</li>
          <li>Ability to self-register with partner approval handled digitally</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>No authority to approve his own account — dependent on partner sign-off</li>
          <li>Dense forms are tiring and error-prone due to dyslexia</li>
          <li>Registration lag means he can't order vaccines during first weeks at a new practice</li>
    </ul>
  </div>

</div>

</div>



---

# WHOLESALER RESPONSIBLE PERSON

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Eleanor Fairclough</h2>
    <p class="job-title">Responsible Person (RP), WDA(H) Wholesaler</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>GPhC-registered since 2005, named RP since 2017</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Manchester</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Pharmaceutical Wholesaler (mid-size, WDA(H))</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>MHRA-named statutory Responsible Person — personal regulatory liability</li>
          <li>Multiple MHRA inspections; navigated post-Brexit GDP transition</li>
          <li>Distributes vaccines and biologicals to NHS trusts, OH firms, private clinics</li>
          <li>Mild hearing impairment in left ear; prefers written confirmation</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Maintain WDA(H) in good standing through every MHRA inspection</li>
          <li>Ensure every ImmForm order has a defensible, timestamped audit trail</li>
          <li>Register multiple staff under one corporate envelope with role-based access</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>E-signatures, timestamped audit logs, and exportable evidence packs</li>
          <li>GDP assurance fields that are enforced, version-stamped, and machine-readable</li>
          <li>Integration with her firm's Quality Management System</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Paper GDP assurances create MHRA regulatory exposure — no version stamping</li>
          <li>Helpdesk email threads would not survive a Chapter 4 GDP records inspection</li>
          <li>Must re-key a separate form per person — no corporate-level batch registration</li>
    </ul>
  </div>

</div>

</div>



---

# IHC PHARMACIST

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Iain MacLeod</h2>
    <p class="job-title">Senior Pharmacist, Immunoglobulin Holding Centre — Band 8a</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>GPhC-registered since 2008, 11 years aseptic services</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Edinburgh</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Teaching Hospital — Aseptic Services / IHC Lead</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Designated lead for Immunoglobulin Holding Centre (HRIG, HNIG, VZIG, HBIG)</li>
          <li>24/7 availability for emergency clinical release via UKHSA RIgS</li>
          <li>Postgraduate diploma in clinical pharmacy</li>
          <li>MHRA WDA(H) / Specials Licence audit responsibility</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Zero stock-outs of HRIG when patients with rabies exposure present to A&E</li>
          <li>Ensure ImmForm account is a compliant regulatory artefact at all times</li>
          <li>Onboard locum pharmacists fast enough for out-of-hours cover</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Structured forms with strict validation (clinical consequence of error)</li>
          <li>IHC-specific terminology in guidance, not routine-immunisation language</li>
          <li>Written confirmation of any phone agreement</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>ImmForm journey designed around routine vaccines, not specialist biological release</li>
          <li>Email approval chain is a poor audit substitute for MHRA inspection</li>
          <li>No reciprocal-trust between trust HR and ImmForm — every new pharmacist starts from zero</li>
    </ul>
  </div>

</div>

</div>



---

# OCCUPATIONAL HEALTH NURSE

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Margaret Findlay</h2>
    <p class="job-title">Occupational Health Nurse — Band 6 equivalent</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>NMC-registered since 1998, 17 years in OH</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Glasgow (Stirlingshire village, patchy mobile)</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Independent OH Provider (SEQOHS-accredited)</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Specialist Community Public Health Nurse (Occupational Health)</li>
          <li>BCG and Tuberculin PPD via ImmForm — the only legal route for private OH</li>
          <li>Works 0.8 WTE (Tue–Fri); clients across HE, construction, care homes</li>
          <li>Presbyopia — benefits from chunked progress indicators</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>No client misses statutory pre-employment TB screening due to ordering delays</li>
          <li>Onboard new OH nurse colleagues quickly when BCG supply restarts</li>
          <li>Maintain cold-chain compliance across multiple client sites</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Forms that recognise private-sector OH context (not NHS-centric language)</li>
          <li>Clear guidance on multi-site delivery-point registration</li>
          <li>Reliable connectivity path — intermittent rural signal at home</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>PDF assumes NHS context — fields like 'Trust' and 'PCN' don't apply to private OH</li>
          <li>5-working-day SLA equals her entire booking lead time for the next BCG clinic</li>
          <li>Must call helpdesk every time to clarify private vs NHS account context</li>
    </ul>
  </div>

</div>

</div>



---

# GP PRACTICE MANAGER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Priya Shah</h2>
    <p class="job-title">Practice Manager — GP Practice</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>14 years in GP administration, 6 as practice manager</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Suburban Leicester</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Cherrywood Medical Centre (~9,800 patients)</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>IGPM Level 5 qualified practice manager</li>
          <li>First language Gujarati; fluent professional English</li>
          <li>Manages flu and routine childhood immunisation orders (~4,000 flu doses per season)</li>
          <li>Reports directly to the senior partner</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>New clinicians have working ImmForm access on day one, not day fifteen</li>
          <li>Keep flu and routine childhood immunisation orders flowing without interruption</li>
          <li>Reduce time from clinician joining to ImmForm access (currently >2 weeks)</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>A streamlined digital registration replacing the PDF + wet-signature process</li>
          <li>Visibility of registration progress without chasing the helpdesk</li>
          <li>Plain-language forms suitable for non-native English readers</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Chasing a GP partner for a wet signature on a PDF for a fortnight</li>
          <li>No visibility into whether registration has been received or is progressing</li>
          <li>QOF-linked income is directly affected by any ordering delay</li>
    </ul>
  </div>

</div>

</div>



---

# SEXUAL HEALTH SERVICE LEAD

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Yusuf Rahman</h2>
    <p class="job-title">Lead Nurse, Sexual Health Service — Band 7</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>NMC-registered since 2012, specialist sexual health</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Central London</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Integrated Sexual Health Service (NHS Trust)</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>PGD signatory for HPV, Hep A/B and Mpox vaccinations</li>
          <li>Runs GBMSM HPV and Mpox programmes from same service</li>
          <li>Bengali-British, raised in Tower Hamlets</li>
          <li>Teaches trust digital-skills induction for new nurses</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Hit JCVI-recommended GBMSM HPV coverage in his catchment</li>
          <li>Maintain Mpox vaccine coverage for high-risk cohort</li>
          <li>No clinic day where a service user is turned away for lack of vaccine</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Register one nurse against two programmes without restating delivery-point details</li>
          <li>Mobile-friendly responsive design (uses trust iPad between clinic rooms)</li>
          <li>Shared service mailbox registration instead of individual nurse addresses</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>In 2022 Mpox response, registration delays caused vial stock-outs for high-risk cohort</li>
          <li>PDF form requires duplicate data entry for two programmes from the same fridge</li>
          <li>Mpox permission mis-alignment causes rejected orders mid-programme</li>
    </ul>
  </div>

</div>

</div>



---

# GP SENIOR PARTNER (APPROVER)

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Dr Helen Vickers</h2>
    <p class="job-title">GP Senior Partner & CQC-Registered Manager</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>GMC-registered since 1996, MRCGP since 2001</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Suburban Leicester</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Cherrywood Medical Centre (same practice as Priya)</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Partner at Cherrywood since 2008; CQC-registered manager</li>
          <li>Two clinical sessions/day (~30 patients each) + admin sessions</li>
          <li>Welsh-English bilingual at home; English at work</li>
          <li>Presbyopia — appreciates large tap targets on mobile</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Staff have access to systems they need to deliver the immunisation contract</li>
          <li>Approve registrations quickly without disrupting clinical sessions</li>
          <li>Governance touches done well, done fast, and over</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Mobile push notification with applicant name, regulator number, and yes/no button</li>
          <li>One-tap mobile approval flow — not a PDF to print, sign, and scan</li>
          <li>Partner-pooling so another partner can approve when she's on leave</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Current PDF assumes she will print, sign, and scan at a desktop — she approves at midnight on her sofa</li>
          <li>No structured time-bound ask — unclear whether emails are real approval requests</li>
          <li>Has had to chase the helpdesk to confirm her own approval was received</li>
    </ul>
  </div>

</div>

</div>



---

# NHS TRUST APPROVER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Marcus Doyle</h2>
    <p class="job-title">Lead Pharmacist for Vaccination Services — Band 8b</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>GPhC since 2005, MSc Clinical Pharmacy, led COVID-19 mass-vax</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>West Midlands</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Large NHS Acute Trust — Pharmacy Governance</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Approves ImmForm registrations for many staff each year</li>
          <li>Led trust's COVID-19 mass-vaccination response 2021–2022</li>
          <li>Power user — expects structured approval queues and dashboards</li>
          <li>Long-sightedness — uses 125% zoom by default</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Defensible governance chain over every trust ImmForm account</li>
          <li>Pass internal pharmacy audit — no orphan accounts, no missing approvals</li>
          <li>Batch-review and timestamp 30+ registrations during flu campaign launch</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Queued, batch-reviewable, timestamped approval workflow — not separate emails</li>
          <li>Delegation capability when on annual leave</li>
          <li>Account revocation through the same system during staff off-boarding</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Informal email approval leaves no audit artefact for internal auditors</li>
          <li>No way to delegate — registrations pile up during leave</li>
          <li>No mechanism to revoke access during off-boarding — leavers' accounts linger</li>
    </ul>
  </div>

</div>

</div>



---

# IMMFORM SERVICE MANAGER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">James Patterson</h2>
    <p class="job-title">Service Delivery Manager, ImmForm Managed Service</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>ITIL-certified, 10 years in NHS-adjacent service delivery</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UK-based, hybrid working</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Managed Service Contractor</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Manages contractual SLAs, ticket volumes, and helpdesk capacity</li>
          <li>Previously ran contact-centre for an NHS Digital service</li>
          <li>Accountable to UKHSA's Service Owner and contractor's Account Director</li>
          <li>Targets 70%+ reduction in account-registration ticket volume</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Reduce registration ticket volume by 70%+ through self-service</li>
          <li>Zero severity-1 incidents during campaign windows</li>
          <li>Helpdesk fights exceptions, not data entry</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>APIs from new onboarding service to ingest events into ITSM tool</li>
          <li>Advance warning of campaign spikes for capacity planning</li>
          <li>Telemetry and observability on any new service's failure modes</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Pandemic volume spikes are unforecastable — can't surge staffing fast enough</li>
          <li>Repetitive registration tickets eat capacity needed for incident resolution</li>
          <li>No structured audit trail — can't defend compliance posture under audit</li>
    </ul>
  </div>

</div>

</div>



---

# IMMFORM HELPDESK AGENT

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Sarah Mitchell</h2>
    <p class="job-title">ImmForm Helpdesk Agent</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>3 years in public-sector helpdesk, former financial services</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Hybrid (mostly home, one office day)</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA ImmForm Managed Service</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Frontline ticket processing — validates PDFs, chases approvers, creates accounts</li>
          <li>Navigates inbox, ticketing system, and admin console daily</li>
          <li>Mild RSI from years of keyboard work; uses vertical mouse</li>
          <li>Keyboard-first navigation preference</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Process registrations accurately within SLA; close tickets cleanly</li>
          <li>Maintain right-first-time rate and minimise back-and-forth cycles</li>
          <li>Keep ticket queue under control — no escalated complaints</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Anything that stops re-keying 20 fields from a PDF into a back-office screen</li>
          <li>Better access to rules upfront (e.g. shared-mailbox prohibition)</li>
          <li>Structured recording of GDP assurances at point of submission</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Manual re-keying from PDFs introduces typos that cost future tickets</li>
          <li>Long email chains to chase approvers — she does the chasing</li>
          <li>Volume spikes (campaign launch, pandemic) overwhelm the team with no automated overflow</li>
    </ul>
  </div>

</div>

</div>



---

# IMMFORM PRODUCT MANAGER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Amrita Chopra</h2>
    <p class="job-title">Product Manager, ImmForm (Grade 7)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Civil-service digital career, ex-engineer</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>London / Birmingham (hybrid)</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Digital, Data & Technology</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Government Digital and Data Profession</li>
          <li>Ran a discovery for a DHSC-adjacent service before joining UKHSA</li>
          <li>Runs design reviews with NVDA and VoiceOver screen readers</li>
          <li>Integrator across product, design, engineering, and compliance</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Median time-to-account-creation under 1 working day for straightforward path</li>
          <li>70%+ ticket reduction for registration; 100% audit-trail completeness</li>
          <li>Pass GDS Service Standard assessment at Alpha</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>A service so boring to use that no-one writes about it</li>
          <li>Reachable subject-matter experts in MHRA GDP and UKHSA logistics</li>
          <li>Analytics and telemetry on the existing portal</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Fragmented user community (7 groups) makes user research expensive</li>
          <li>Riskiest assumption (every applicant has a digitally-reachable approver) is hard to validate</li>
          <li>Compliance hand-offs are slow — no definitive answer on audit trail requirements</li>
    </ul>
  </div>

</div>

</div>



---

# SENIOR USER RESEARCHER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Theo Brennan</h2>
    <p class="job-title">Senior User Researcher, ImmForm (SEO/G7)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Service designer, previously at GDS</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UK-based, hybrid + field visits</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Digital, Data & Technology</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Trained as a service designer in central government, previously at GDS</li>
          <li>Passionate advocate for GDS Point 1: Understand users and their needs</li>
          <li>Mild deuteranomaly colour-blindness — informs testing protocols</li>
          <li>Recruits with translators for non-English-fluent participants</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Build qualitative and quantitative evidence across all 7 user groups</li>
          <li>GDS assessor concludes Point 1 is met</li>
          <li>Every applicant — including assisted-digital users — completes the journey first time</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Research sessions across all user groups with diverse participant recruitment</li>
          <li>Access to MHRA / GDP context experts to interpret regulated-system constraints</li>
          <li>Helpdesk telemetry on how many registrations fail and why</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Wholesalers and IHC pharmacists are tiny populations — hard to recruit</li>
          <li>Applicants reluctant to engage in research during campaign launch</li>
          <li>Accessibility recruitment costs more and risks being squeezed under delivery pressure</li>
    </ul>
  </div>

</div>

</div>



---

# UKHSA GDP COMPLIANCE LEAD

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Dr Olu Babatunde</h2>
    <p class="job-title">UKHSA GDP Compliance Lead / Responsible Person (G7/SCS)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>GPhC-registered pharmacist, MHRA-named RP</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UK-based</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Vaccines & Countermeasures</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>UKHSA Responsible Person for centrally procured medicinal products</li>
          <li>Regulates under WDA(H) and EU/UK GDP (MHRA Guidance Note 6)</li>
          <li>Postgraduate quality-management qualifications</li>
          <li>Personal regulatory liability under Human Medicines Regulations 2012</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>ImmForm onboarding produces an audit trail acceptable to MHRA inspection</li>
          <li>Clean MHRA inspection — UKHSA's wholesale operation in good standing</li>
          <li>Every GDP assurance timestamped, attributable, and exportable</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Enforced confirmation (no nullable assurance fields)</li>
          <li>Version-stamped records and exportable JSON/PDF evidence packs</li>
          <li>Role-based access control with separation of duties</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Today's email-thread audit trail is non-compliant for MHRA Chapter 4 records</li>
          <li>Paper assurances on the existing PDF give no defence in an inspection</li>
          <li>Often pulled into design at late stages rather than from discovery onward</li>
    </ul>
  </div>

</div>

</div>



---

# UKHSA DATA PROTECTION OFFICER

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Rachel Goldstein</h2>
    <p class="job-title">Data Protection Officer & Head of IG, UKHSA</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Solicitor (non-practising), IAPP CIPP/E and CIPM-certified</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>UK-based</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA Information Governance</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Statutory DPO under UK GDPR for UKHSA</li>
          <li>Former NHS IG manager with extensive ICO experience</li>
          <li>Hard-of-hearing in noisy environments; uses Teams live captions</li>
          <li>Manages DPIAs, breach response, ROPA, and Subject Access Requests</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Redesign meets UK GDPR / DPA 2018 and NHS DSPT standards</li>
          <li>No ICO enforcement; DPIA accepted; no breaches from the new service</li>
          <li>Privacy notice the applicant actually reads — plain English</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Strong audit logging, role-based access, and retention enforcement</li>
          <li>Secure transit (TLS) and minimum-necessary data collection</li>
          <li>Briefings on new design's data flows and processor contracts</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>PDF-and-email workflow scatters personal data across mailboxes</li>
          <li>No structured retention policy on the email artefacts that are the audit trail</li>
          <li>Applicants asked for personal data (e.g. mobile numbers) without clear lawful basis</li>
    </ul>
  </div>

</div>

</div>



---

# UKHSA HEAD OF IMMUNISATION

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Dr Catriona Lewis</h2>
    <p class="job-title">Head of Immunisation & Vaccine Preventable Diseases (SCS)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Public health physician (FFPH), published author</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>London / Whitehall</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>UKHSA — Senior Civil Service</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Consultant in communicable disease control, previously at PHE</li>
          <li>Oversees all national immunisation programme coverage rates</li>
          <li>Works with JCVI, ministerial briefings, and sector engagement</li>
          <li>Scottish; 55–65 hours/week</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>ImmForm onboarding should never be the bottleneck delaying a national programme launch</li>
          <li>Coverage rates rise; no avoidable outbreaks; new programmes deploy on time</li>
          <li>Sustain and improve immunisation coverage across all national programmes</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Operational metrics visible on a single page — briefings in plain English</li>
          <li>Trust in operational delivery so she can focus on strategy</li>
          <li>Advance warning of digital bottlenecks before they become ministerial issues</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Surprise bottlenecks discovered from Cabinet Office post-incident reviews</li>
          <li>Limited visibility into operational metrics of supporting digital services</li>
          <li>Coordinating across UKHSA, NHSE, DHSC, MHRA, and ICBs for programme launches</li>
    </ul>
  </div>

</div>

</div>



---

# LOCAL AUTHORITY DPH

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Dr Aisha Bello</h2>
    <p class="job-title">Director of Public Health, London Borough Council</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Public health consultant (FFPH), led borough COVID-19 response</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>London</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>London Borough Council — Statutory Chief Officer</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Statutory chief officer under s.30 Health and Social Care Act 2012</li>
          <li>Commissioner of sexual health services (Local Authority statutory duty)</li>
          <li>Yoruba-English bilingual; serves multilingual / digitally excluded communities</li>
          <li>50–60 hours/week; commutes on the Tube</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Coverage rises across deprivation deciles; outbreaks contained early</li>
          <li>Commissioned services deliver GBMSM and Mpox vaccines without onboarding bottlenecks</li>
          <li>Protect and improve health of her local population</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>ImmForm coverage data dashboards for her local intelligence</li>
          <li>Consistent digital maturity across commissioned providers</li>
          <li>Reduced onboarding friction in commissioned services</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Variable provider digital maturity — small charities, OH providers, hospices</li>
          <li>Onboarding bottlenecks in commissioned services degrade commissioning outcomes</li>
          <li>Information fragmented across UKHSA, NHSE, and borough intelligence teams</li>
    </ul>
  </div>

</div>

</div>



---

# DHSC IMMUNISATION POLICY LEAD

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Dr Charlotte Penrose</h2>
    <p class="job-title">Deputy Director, Immunisation Policy — DHSC (SCS1)</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Career civil servant, cross-departmental policy</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>London / Whitehall</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>Department of Health and Social Care</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Senior Civil Service (Deputy Director, SCS Pay Band 1)</li>
          <li>Led immunisation-policy response post-COVID</li>
          <li>Cross-government coordination with JCVI, Treasury, devolved administrations</li>
          <li>55–65 hours/week; Whitehall idiom</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Policy environment supports UKHSA and NHSE in delivering immunisation programmes</li>
          <li>Ministers can answer parliamentary questions confidently</li>
          <li>No ImmForm bottleneck becomes a Times Health Commission feature</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>UKHSA to flag operational risk early — not after it becomes news</li>
          <li>Clear briefings on digital plumbing risks</li>
          <li>Confidence that delivery failures won't land as ministerial reputational issues</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Operational delivery failures landing as ministerial reputational issues</li>
          <li>Limited visibility into digital plumbing — relies on UKHSA to flag risk</li>
          <li>Policy commitments at risk when onboarding bottlenecks delay programme delivery</li>
    </ul>
  </div>

</div>

</div>



---

# NHSE REGIONAL VACCINATIONS LEAD

<div class="persona-content">

<div class="profile-column">
  <div class="profile-section">
  <img src="../images/placeholder.jpg" alt="Profile Photo" />
    <h2 class="persona-name">Steve Mukherjee</h2>
    <p class="job-title">Regional Director, Vaccinations & Screening — Midlands</p>
    <div class="personal-data">
      <div>
        <h4>Professional Data</h4>
        <div class="persona-facts">
          <div class="data-item"><span class="data-icon">👤</span><span>Public health background, led Midlands COVID-19 vax response</span></div>
          <div class="data-item"><span class="data-icon">📍</span><span>Birmingham / Midlands</span></div>
          <div class="data-item"><span class="data-icon">💼</span><span>NHS England — Regional Senior Leadership</span></div>
        </div>
      </div>
      <div>
        <h4>Role & Background</h4>
        <ul>
          <li>Manages ICB delegation transition for vaccinations and screening</li>
          <li>Ran the Midlands COVID-19 vaccination response</li>
          <li>British-Bengali heritage; English first language professionally</li>
          <li>55+ hours/week; reads on the train into Birmingham</li>
        </ul>
      </div>
    </div>
  </div>

  <div class="logo-container">
    <img src="../images/ukhsa-logo.png" alt="UKHSA logo" />
    <img src="../images/microsoft-logo.png" alt="Microsoft logo" />
  </div>
</div>

<div class="content-single-column">
  
  <div class="content-box">
    <h3 class="section-title">Goals & Desired Outcomes</h3>
    <ul>
          <li>Achieve commissioned coverage targets across his region</li>
          <li>Smooth campaign launches with no coverage gaps</li>
          <li>Manage the ICB delegation transition without disruption</li>
    </ul>
  </div>
  
  <div class="content-box">
    <h3 class="section-title">Wants & Needs</h3>
    <ul>
          <li>Providers able to onboard staff faster than NHS Smartcard issuance</li>
          <li>Regional-level view of ImmForm onboarding throughput</li>
          <li>Dashboards over emails for performance monitoring</li>
    </ul>
  </div>

  <div class="content-box">
    <h3 class="section-title">Pain Points & Frustrations</h3>
    <ul>
          <li>Onboarding bottlenecks cascade through commissioned providers into coverage figures</li>
          <li>No regional-level view of ImmForm onboarding throughput</li>
          <li>Variable digital maturity across providers in his region</li>
    </ul>
  </div>

</div>

</div>
